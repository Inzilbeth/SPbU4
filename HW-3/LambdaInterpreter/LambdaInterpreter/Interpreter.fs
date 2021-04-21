namespace LambdaInterpreter.Interpreter

open System

type Term =
    | Variable of char
    | Abstraction of char * Term
    | Application of Term * Term

/// Lambda interpreter
module Interpreter =

    let random = Random()

    /// Substitution
    let rec subst v leftTerm rightTerm =
        match rightTerm with
        | Variable rightVar when rightVar = v -> leftTerm
        | Variable _ -> rightTerm
        | Abstraction (x, abstTerm) ->

            let getFreeVariables term =
                let rec loop acc term =
                    match term with
                    | Variable v -> Set.add v acc
                    | Abstraction (v, abstTerm) -> Set.remove v <| loop acc abstTerm
                    | Application (applLeftTerm, applRightTerm) ->
                        (loop acc applLeftTerm) + (loop acc applRightTerm)
                loop Set.empty term

            let getUniqueVariable (set: Set<char>) =
                let chars = Array.concat([[|'a' .. 'z'|];[|'A' .. 'Z'|];[|'0' .. '9'|]])
                let size = Array.length chars in

                let rec loop char =
                    if set.Contains char then
                        loop chars.[random.Next size]
                    else
                        char

                loop chars.[random.Next size]

            match leftTerm with
            | Variable x when x = v -> rightTerm
            | _ when getFreeVariables leftTerm |> Set.contains x |> not
                  || getFreeVariables abstTerm |> Set.contains v |> not ->
                     Abstraction (x, subst v leftTerm abstTerm)
            | _ -> let y = getFreeVariables abstTerm + getFreeVariables leftTerm
                         |> getUniqueVariable
                   Abstraction (
                       y, abstTerm
                       |> subst x (Variable y)
                       |> subst v leftTerm)

        | Application (applLeftTerm, applRightTerm) ->
            Application (
                subst v leftTerm applLeftTerm,
                subst v leftTerm applRightTerm)

    /// Reduction of term
    let rec reduce term =
        match term with
        | Variable _ -> term
        | Abstraction (v, abstTerm) -> Abstraction (v, reduce abstTerm)
        | Application (leftTerm, rightTerm) ->
            match leftTerm with
            | Abstraction (v, abstTerm) -> reduce <| subst v rightTerm abstTerm
            | _ -> Application (reduce leftTerm, reduce rightTerm)
