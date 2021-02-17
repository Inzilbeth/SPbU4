let factorial x =
    if x < 0I
    then invalidArg "x" $"Invalid input: {x}. Input value must be nonnegative."

    let rec factorialRec x acc =
        if x <= 1I
        then acc
        else factorialRec (x - 1I) x * acc

    factorialRec x 1I

printfn "Factorial of 20: %A, factorial of 0: %A." (factorial 20I) (factorial 0I)
