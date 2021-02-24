namespace MapForTrees.TreeMap

type Tree<'t> =
    | Tree of 't * Tree<'t> * Tree<'t>
    | Tip of 't

module TreeMap =
    let rec mapTree func tree =
        match tree with
        | Tree(node, left, right) ->
            Tree(func node, mapTree func left, mapTree func right)
        | Tip(node) -> Tip(func node)