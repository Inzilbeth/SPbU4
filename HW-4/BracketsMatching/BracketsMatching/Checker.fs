namespace BracketsMatching.Checker

module Checker =

    let check (str:string) =
        let chars = Seq.toList str

        let rec checkLoop (charSequence:List<char>) (count:List<char>) =
            match (charSequence, count) with
            | c :: tail, count when c = '(' || c = '[' || c = '{' -> checkLoop tail (c :: count)
            | c1 :: tail, c2 :: count when (c1 = ')' && c2 = '(') ||
                                           (c1 = ']' && c2 = '[') ||
                                           (c1 = '}' && c2 = '{') -> checkLoop tail count
            | c :: _, _ when c = ')' || c = ']' || c = '}' -> false
            | _ :: tail, count -> checkLoop tail count
            | [], [] -> true
            | [], _ -> false


        checkLoop chars []
