
namespace MathFunctionsFSharp

module MathFunctions =
    open System

    let rec fibonacci currentInt:int = 
        match currentInt with
        | 0 -> 1
        | 1 -> 1
        | _ -> 
            match currentInt < 0 with
            | true -> 0
            | false -> fibonacci(currentInt-1) + fibonacci(currentInt-2)
            

    let rec factorial currentInt:int = 
        match currentInt with
        | 0 -> 1
        | 1 -> 1
        | _ -> 
            match currentInt < 0 with
            | true -> 0
            | false -> currentInt * factorial(currentInt-1)

    
        
            
    