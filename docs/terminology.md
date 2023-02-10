

# 1. Basic Graph Definition

Agraph is a **symbolic representation** of a network and its connectivity. It implies an [abstraction of reality](https://transportgeography.org/?page_id=5992) so that it can be simplified as a set of linked nodes. The origins of graph theory can be traced to Leonhard Euler, who devised in 1735 a problem that came to be known as the “Seven Bridges of Konigsberg”. In this problem, someone had to cross all the bridges only once, and in a continuous sequence, a problem the Euler proved to have no solution by representing it as a set of nodes and links. This led to the foundation of graph theory and its subsequent improvements. It has been enriched in the last decades by growing influences from studies of social and complex networks.

In transport geography, most networks have an obvious spatial foundation, namely road, transit, and rail networks, which tend to be defined more by their links than by their nodes. This is not necessarily the case for all transportation networks. For instance, maritime and air networks tend to be more defined by their nodes than by their links since links are often not clearly defined. A telecommunication system can also be represented as a network, while its spatial expression can have limited importance and would be difficult to represent. Mobile telephone networks or the Internet, possibly to most complex graphs to be considered, are relevant cases of networks having a structure that can be difficult to symbolize. However, cellular phones and antennas can be represented as nodes, while the links could be individual phone calls. Servers, the core of the Internet, can also be represented as nodes within a graph while the physical infrastructure between them, namely fiber optic cables, can act as links. Consequently, all transport networks can be represented by graph theory in one way or the other.

The following elements are fundamental to understanding graph theory:

> **[Graph](https://transportgeography.org/?page_id=5998).** A graph _G_ is a set of vertices (nodes) _v_ connected by edges (links) _e_. Thus _G=(v, e)_.

> **Vertex (Node).** A node _v_ is a terminal point or an intersection point of a graph. It is the abstraction of a location such as a city, an administrative division, a road intersection or a transport terminal (stations, terminuses, harbors and airports).

> **Edge (Link).** An edge _e_ is a link between two nodes. The link (_i_, _j_) is of initial extremity _i_ and of terminal extremity _j_. A link is the abstraction of a transport infrastructure supporting movements between nodes. It has a direction that is commonly represented as an arrow. When an arrow is not used, it is assumed the link is bi-directional.

> **Sub-Graph.** A sub-graph is a subset of a graph _G_ where _p_ is the number of sub-graphs. For instance, _G’_ = (_v’_, _e’_) can be a distinct sub-graph of _G_. Unless the global transport system is considered in its whole, every transport network is in theory a sub-graph of another. For instance, the road transportation network of a city is a sub-graph of a regional transportation network, which is itself a sub-graph of a national transportation network.

> **Buckle (Loop or self edge).** A link that makes a node correspond to itself is a buckle.

> **[Planar Graph](https://transportgeography.org/?page_id=6003).** A graph where all the intersections of two edges are a vertex. Since this graph is located within a plane, its topology is two-dimensional. This is typically the case for power grids, road and railway networks, although great care must be inferred to the definition of nodes (terminals, warehouses, cities).

> **[Non-planar Graph](https://transportgeography.org/?page_id=6003).** A graph where there are no vertices at the intersection of at least two edges. Networks that can be considered in a planar fashion, such as roads, can be represented as non-planar networks. This implies a third dimension in the topology of the graph since there is the possibility of having a movement “passing over” another movement such as for air and maritime transport, or an overpass for a road. A non-planar graph has potentially much more links than a planar graph.

> **[Simple graph](https://transportgeography.org/?page_id=6007)**. A graph that includes only one type of link between its nodes. A road or rail network are simple graphs.

> **[Multigraph](https://transportgeography.org/?page_id=6007)**. A graph that includes several types of links between its nodes. Some nodes can be connected to one link type while others can be connected to more than one that are running in parallel. A graph depicting a road and a rail network with different links between nodes serviced by either or both modes is a multigraph.


# 2. Links and their Structures

A transportation network enables flows of people, freight or information, which are occurring along its links. Graph theory must thus offer the possibility of representing movements as linkages, which can be considered over several aspects:

> **[Connection](https://transportgeography.org/?page_id=6013).** A set of two nodes as every node is linked to the other. Considers if a movement between two nodes is possible, whatever its direction. Knowing connections makes it possible to find if it is possible to reach a node from another node within a graph.

> **[Path](https://transportgeography.org/?page_id=6013).** A sequence of links that are traveled in the same direction. For a path to exist between two nodes, it must be possible to travel an uninterrupted sequence of links. Finding all the possible paths in a graph is a fundamental attribute in measuring accessibility and traffic flows.

> **Chain.** A sequence of links having a connection in common with the other. Direction does not matter.

> **[Length of a Link, Connection or Path](https://transportgeography.org/?page_id=6018).** Refers to the label associated with a link, a connection or a path. This label can be distance, the amount of traffic, the capacity or any relevant attribute of that link. The length of a path is the number of links (or connections) in this path.

> **[Cycle](https://transportgeography.org/?page_id=6023).** Refers to a chain where the initial and terminal node is the same and that does not use the same link more than once is a cycle.

> **[Circuit](https://transportgeography.org/?page_id=6023).** A path where the initial and terminal node corresponds. It is a cycle where all the links are traveled in the same direction. Circuits are very important in transportation because several distribution systems are using circuits to cover as much territory as possible in one direction (delivery route).

> **Clique**. A clique is a maximal complete subgraph where all vertices are connected.

> **Cluster**. Also called community, it refers to a group of nodes having denser relations with each other than with the rest of the network. A wide range of methods are used to reveal clusters in a network, notably they are based on modularity measures (intra- versus inter-cluster variance).

> **[Ego network](https://transportgeography.org/?page_id=6028)**. For a given node, the ego network corresponds to a sub-graph where only its adjacent neighbors and their mutual links are included.

> **[Nodal region](https://transportgeography.org/?page_id=6032)**. A nodal region refers to a subgroup (tree) of nodes polarized by an independent node (which largest flow link connects a smaller node) and several subordinate nodes (which largest flow link connects a larger node). Single or multiple linkage analysis methods are used to reveal such regions by removing secondary links between nodes while keeping only the heaviest links.

> **[Dual graph](https://transportgeography.org/?page_id=6038)**. A method in space syntax that considers edges as nodes and nodes as edges. In urban street networks, large avenues made of several segments become single nodes while intersections with other avenues or streets become links (edges). This method is particularly useful to reveal hierarchical structures in a planar network.

> **Common neighbor**. For two or more nodes, the number of nodes that they are commonly connected two.


# 3. Basic Structural Properties

The organization of nodes and links in a graph conveys a structure that can be described and labeled. The basic structural properties of a graph are:

> **Symmetry and Asymmetry**. A graph is symmetrical if each pair of nodes linked in one direction is also linked in the other. By convention, a line without an arrow represents a link where it is possible to move in both directions. However, both directions have to be defined in the graph. Most transport systems are symmetrical, but asymmetry can often occur as it is the case for maritime (pendulum) and air services. Asymmetry is rare on road transportation networks, unless one-way streets are considered.

> **Assortativity and disassortativity**. Assortative networks are those characterized by relations among similar nodes, while disassortative networks are found when structurally different nodes are often connected. Transport (or technological) networks are often disassortative when they are non-planar, due to the higher probability for the network to be centralized into a few large hubs.

> **Completeness.** A graph is complete if two nodes are linked in at least one direction. A complete graph has no sub-graph and all its nodes are interconnected.

> **[Connectivity](https://transportgeography.org/?page_id=6043).** A complete graph is described as connected if for all its distinct pairs of nodes there is a linking chain. Direction does not have importance for a graph to be connected but may be a factor for the _level_ of connectivity. If _p_>1 the graph is not connected because it has more than one sub-graph (or component). There are various levels of connectivity, depending on the degree at which each pair of nodes is connected.

> **[Complementarity](https://transportgeography.org/?page_id=6049).** Two sub graphs are complementary if their union results in a complete graph. Multimodal transportation networks are complementary as each sub-graph (modal network) benefits from the connectivity of other sub-graphs.

> **[Root](https://transportgeography.org/?page_id=6054)**. A node _r_ where every other node is the extremity of a path coming from _r_ is a root. Direction has an importance. A root is generally the starting point of a distribution system, such as a factory or a warehouse.

> **[Trees](https://transportgeography.org/?page_id=6060)**. A connected graph without a cycle is a tree. A tree has the same number of links than nodes plus one. (_e = v-1_). If a link is removed, the graph ceases to be connected. If a new link between two nodes is provided, a cycle is created. A branch of root _r_ is a tree where no links are connecting any node more than once. River basins are typical examples of tree-like networks based on multiple sources connecting towards a single estuary. This structure strongly influences [river transport systems](https://transportgeography.org/?page_id=1782).

> **[Articulation Node](https://transportgeography.org/?page_id=6065).** In a connected graph, a node is an articulation node if the sub-graph obtained by removing this node is no longer connected. It therefore contains more than one sub-graph (_p_ > 1). An articulation node is generally a port or an airport, or an important hub of a transportation network, which serves as a bottleneck. It is also called a bridge node.

> **[Isthmus](https://transportgeography.org/?page_id=6071).** In a connected graph, an isthmus is a link that is creating, when removed, two sub-graphs having at least one connection. Most central links in a complex network are often isthmuses, which removal by reiteration helps revealing dense communities (clusters).