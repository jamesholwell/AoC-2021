module AoC2021.FSharp.Shared

    open System

    let Split (s: string) = s.Split ("\n", StringSplitOptions.RemoveEmptyEntries)
    
    let SplitInt = Split >> Seq.map int
