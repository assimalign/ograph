# OGraph golden fixtures

Golden request/response fixtures for the OGraph communication binding (specification
[§4](../specification.md#40--protocol-bindings)) and response envelope
([§6](../specification.md#60--communication)). They are the executable seed for the **N-03
conformance suite** and are consumed as-is by server, client, and schema tests.

Each fixture is a self-contained JSON document with this shape:

```jsonc
{
  "name":  "kebab identifier",
  "title": "one-line title",
  "description": "what this fixture pins",
  "spec": ["§4.3", "§6.2"],          // specification sections exercised
  "conformance": ["V-01", "V-05"],   // roadmap features seeded
  "request":  { "method", "route", "headers", "body" },
  "response": { "status", "headers", "envelope" }
}
```

- **`request.body`** is the OGraph query text sent with media type
  `application/vnd.ograph.query` (or `null` for `GET`/`OPTIONS`).
- **`response.envelope`** is the parsed body of media type `application/vnd.ograph+json`.
  It **MUST** validate against [`../schemas/ograph-response.schema.json`](../schemas/ograph-response.schema.json)
  (JSON Schema draft 2020-12). When the response carries no body (a `304` or `204`),
  `response.envelope` is `null` and there is nothing to validate.

## Validation

A fixture is conformant when (a) its `response.envelope`, if non-null, validates against the
response-envelope schema, and (b) a server under test, given `request`, produces `response`
(status, the asserted headers, and an envelope equal modulo server-assigned identity such as
absolute URIs and validator opaque strings). Validate the envelopes with any draft 2020-12
validator, e.g.:

```python
import json, glob
from jsonschema import Draft202012Validator
schema = json.load(open("docs/schemas/ograph-response.schema.json", encoding="utf-8"))
Draft202012Validator.check_schema(schema)
v = Draft202012Validator(schema)
for fp in sorted(glob.glob("docs/fixtures/*.json")):
    env = json.load(open(fp, encoding="utf-8"))["response"]["envelope"]
    if env is not None:
        v.validate(env)
```

Last validated run: **schema valid (draft 2020-12); 10 envelope-bearing fixtures pass, 2
body-less fixtures skipped, 0 failures.**

## Manifest

| # | Fixture | Method | Status | Covers | Seeds |
| --- | --- | --- | --- | --- | --- |
| 01 | [`01-basic-project.json`](01-basic-project.json) | QUERY | 200 | Primary QUERY binding; request/response media types; single-page collection with `$count`/`$total` | V-01, V-02, V-05 |
| 02 | [`02-filter-sort-page.json`](02-filter-sort-page.json) | QUERY | 200 | `.filter()` + `.sort()` + offset `.page()`; paging metadata | V-01, V-05 |
| 03 | [`03-edge-traversal-nested.json`](03-edge-traversal-nested.json) | QUERY | 200 | Two-hop `.edge()` traversal; recursive `$edges`; nested single-cardinality result | V-05, V-06 |
| 04 | [`04-partial-failure-edge.json`](04-partial-failure-edge.json) | QUERY | 200 | Per-edge partial failure — one edge `$status` 500 under a 200 root | V-05, V-06 |
| 05 | [`05-empty-result.json`](05-empty-result.json) | QUERY | 200 | Empty collection (`$count`/`$total` 0, `$nodes` `[]`) — not an error | V-05 |
| 06 | [`06-400-syntax-error.json`](06-400-syntax-error.json) | QUERY | 400 | Malformed query body → 400 with G-code + source position | V-02, V-03 |
| 07 | [`07-415-unsupported-media-type.json`](07-415-unsupported-media-type.json) | QUERY | 415 | Unsupported query media type → 415 + `Accept-Query` | V-02, V-04 |
| 08 | [`08-422-policy-violation.json`](08-422-policy-violation.json) | QUERY | 422 | Well-formed query fails policy validation → 422 diagnostic | V-03 |
| 09 | [`09-conditional-304.json`](09-conditional-304.json) | QUERY | 304 | Conditional QUERY with `If-None-Match` → 304, no body | V-08 |
| 10 | [`10-get-fallback.json`](10-get-fallback.json) | GET | 200 | OPTIONAL `GET ?query=` fallback profile; identical envelope | V-01, N-03 (optional profile) |
| 11 | [`11-accept-query-options.json`](11-accept-query-options.json) | OPTIONS | 204 | `OPTIONS` discovery advertising `Allow` + `Accept-Query`, no body | V-04 |
| 12 | [`12-cursor-page.json`](12-cursor-page.json) | QUERY | 200 | Cursor-mode paging metadata (`$cursor.next`, `$total` null) | V-05 |

### Required-coverage checklist (issue #23)

| Required scenario | Fixture |
| --- | --- |
| basic project | 01 |
| filter + sort + page | 02 |
| edge traversal with nested project | 03 |
| per-edge partial failure (edge 500, root 200) | 04 |
| empty result | 05 |
| 400 syntax error | 06 |
| 415 wrong media type | 07 |
| 422 policy violation | 08 |
| conditional request (ETag / 304) | 09 |
| GET-fallback profile | 10 |

## Notes on fidelity

- URIs are shown as path-absolute references for portability; a live server emits absolute
  URIs. A conformance runner comparing responses treats server-assigned identity (absolute
  URI authority, opaque `ETag`/cursor strings) as non-significant.
- Query bodies use illustrative filter operators (`eq`, `desc`, …); the committed operator and
  function vocabulary is owned by S-03 and does not change the envelope shape these fixtures pin.
