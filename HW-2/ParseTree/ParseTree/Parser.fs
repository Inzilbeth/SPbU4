namespace ParseTree.Parser

type Expression =
    | Number of int
    | Add of Expression * Expression
    | Sub of Expression * Expression
    | Div of Expression * Expression
    | Mult of Expression * Expression

module Parser =
    let rec parse expression =
        match expression with
        | Number x -> x
        | Add(e1, e2) -> (parse e1) + (parse e2)
        | Sub(e1, e2) -> (parse e1) - (parse e2)
        | Div(e1, e2) ->
            if (e2 = Number 0) then
                invalidOp "Division by zero occured!"
            else
                (parse e1) / (parse e2)
        | Mult(e1, e2) -> (parse e1) * (parse e2)