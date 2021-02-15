let contFactorial x =
    if x < 0I then
        failwith $"Invalid input: {x}. Input value must be nonnegative."

    let rec contTailRecursiveFactorial x f =
        if x <= 1I then
            f()
        else
            contTailRecursiveFactorial (x - 1I) (fun () -> x * f())

    contTailRecursiveFactorial x (fun () -> 1I)

printfn "Factorial of 20: %A, factorial of 0: %A." (contFactorial 20I) (contFactorial 0I)
