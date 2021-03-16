namespace BracketsMatching.Checker

module Checker =

    let check (str:string) =
        let chars = Seq.toList str

        let rec checkLoop (charSequence:List<char>) (count:List<char>) (op:char) (cl:char) =
            match (charSequence, count) with
            | c :: tail,  count when c = op -> checkLoop tail (op :: count) op cl
            | c1 :: tail, c2 :: count when c1 = cl && c2 = op -> checkLoop tail count op cl
            | c :: _, _ when c = cl -> false
            | _ :: tail, count -> checkLoop tail count op cl
            | [], [] -> true
            | [], _ -> false

        (* Calcualates in 3*n but otherwise match is too big and repetitive :( *)
        checkLoop chars [] '(' ')' && checkLoop chars [] '[' ']' && checkLoop chars [] '{' '}'