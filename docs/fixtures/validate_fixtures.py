#!/usr/bin/env python3
"""Validate the OGraph golden fixtures.

Two independent checks are applied to every fixture in this directory:

1. **Schema validation** — each non-null ``response.envelope`` MUST validate
   against ``../schemas/ograph-response.schema.json`` (JSON Schema draft 2020-12).

2. **Position consistency** — every diagnostic ``position`` block is an asserted,
   deterministic field (the README exempts only server-assigned identity from
   comparison, not positions). Because the schema declares positions *zero-based*,
   the ``line``/``column`` a fixture carries MUST equal the line/column recomputed
   from its own ``offset`` against the exact ``request.body``. This guards against
   the class of drift where ``offset``, ``line`` and ``column`` point at different
   characters or mix one-based and zero-based numbering.

Exit status is non-zero if any check fails, so this can be wired into CI.

Usage:
    python docs/fixtures/validate_fixtures.py
"""
from __future__ import annotations

import glob
import json
import os
import sys

from jsonschema import Draft202012Validator

HERE = os.path.dirname(os.path.abspath(__file__))
SCHEMA_PATH = os.path.normpath(os.path.join(HERE, "..", "schemas", "ograph-response.schema.json"))


def line_col_from_offset(body: str, offset: int) -> tuple[int, int]:
    """Return the zero-based (line, column) for ``offset`` within ``body``.

    Lines are split on ``\\n``; ``column`` is the zero-based count of characters
    from the start of the line to ``offset``.
    """
    if offset < 0 or offset > len(body):
        raise ValueError(f"offset {offset} is out of range for a body of length {len(body)}")
    prefix = body[:offset]
    line = prefix.count("\n")
    last_nl = prefix.rfind("\n")
    column = offset - (last_nl + 1)
    return line, column


def iter_positions(node):
    """Yield every diagnostic dict that carries a ``position`` block, recursively."""
    if isinstance(node, dict):
        if "position" in node and isinstance(node["position"], dict):
            yield node
        for value in node.values():
            yield from iter_positions(value)
    elif isinstance(node, list):
        for item in node:
            yield from iter_positions(item)


def check_positions(fixture: dict) -> list[str]:
    """Return a list of human-readable position-consistency errors for one fixture."""
    errors: list[str] = []
    body = ((fixture.get("request") or {}).get("body"))
    envelope = (fixture.get("response") or {}).get("envelope")
    if envelope is None:
        return errors
    for diag in iter_positions(envelope):
        pos = diag["position"]
        offset = pos.get("offset")
        line = pos.get("line")
        column = pos.get("column")
        if offset is None or line is None or column is None:
            # A partial position block asserts nothing to cross-check.
            continue
        if body is None:
            errors.append(
                f"diagnostic {diag.get('code')!r} carries a position but request.body is null"
            )
            continue
        try:
            exp_line, exp_col = line_col_from_offset(body, offset)
        except ValueError as exc:
            errors.append(f"diagnostic {diag.get('code')!r}: {exc}")
            continue
        if (line, column) != (exp_line, exp_col):
            errors.append(
                f"diagnostic {diag.get('code')!r}: offset {offset} maps to "
                f"zero-based line {exp_line}, column {exp_col}, but the fixture "
                f"asserts line {line}, column {column}"
            )
    return errors


def main() -> int:
    schema = json.load(open(SCHEMA_PATH, encoding="utf-8"))
    Draft202012Validator.check_schema(schema)
    validator = Draft202012Validator(schema)

    schema_ok = 0
    schema_skipped = 0
    failures: list[str] = []

    for fp in sorted(glob.glob(os.path.join(HERE, "*.json"))):
        name = os.path.basename(fp)
        fixture = json.load(open(fp, encoding="utf-8"))

        envelope = (fixture.get("response") or {}).get("envelope")
        if envelope is None:
            schema_skipped += 1
        else:
            for err in validator.iter_errors(envelope):
                failures.append(f"{name}: schema: {err.message} (at {list(err.absolute_path)})")
            else:
                schema_ok += 1

        for msg in check_positions(fixture):
            failures.append(f"{name}: position: {msg}")

    if failures:
        print("FIXTURE VALIDATION FAILED:")
        for f in failures:
            print(f"  - {f}")
        return 1

    print(
        f"schema valid (draft 2020-12); {schema_ok} envelope-bearing fixtures pass, "
        f"{schema_skipped} body-less fixtures skipped, position checks pass, 0 failures."
    )
    return 0


if __name__ == "__main__":
    sys.exit(main())
