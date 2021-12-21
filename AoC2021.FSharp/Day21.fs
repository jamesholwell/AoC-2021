namespace AoC2021.FSharp

open AoC2021.Core

module Day21 =
    type GameRules = {
        BoardSize: int
        Die: seq<int>
        NumberOfRolls: int
        TargetScore: int
    }
    
    type PlayerState = {
        Name: string
        Location: int // 1-indexed
        Score: int
        Rolls: int
    }
    
    let Parse s =
        let positions = Shared.Split s |> Array.map (fun s -> s.Substring(s.Length - 2) |> int) 
        (positions[0], positions[1])
        
    let DeterministicDie sides =
        Seq.initInfinite (fun x -> 1 + x % sides)
        
    let DieRoller n (die : seq<int>)  =
        die |> Seq.chunkBySize n |> Seq.map Array.sum
        
    let TakeTurn rules state roll =
        let newLocation = (state.Location - 1 + roll) % rules.BoardSize + 1
        {
            Name = state.Name
            Location = newLocation
            Score = state.Score + newLocation
            Rolls = state.Rolls + rules.NumberOfRolls 
        }
    
    let PlayGame rules player1 player2 =
        let roller = rules.Die |> DieRoller rules.NumberOfRolls
        let rec loop roller thisPlayer otherPlayer =
            let roll = Seq.head roller
            let newState = TakeTurn rules thisPlayer roll
            if (newState.Score >= rules.TargetScore) then
                rules, newState, otherPlayer
            else
                loop (Seq.tail roller) otherPlayer newState
        loop roller player1 player2
    
    let PlayPartOne (pl1, pl2) =
        let rules = {
            BoardSize = 10
            Die = DeterministicDie 100
            NumberOfRolls = 3
            TargetScore = 1000
        }
        let player1 = { Name = "Player 1"; Location = pl1 ; Score = 0 ; Rolls = 0 }
        let player2 = { Name = "Player 2"; Location = pl2 ; Score = 0 ; Rolls = 0 }
        PlayGame rules player1 player2
    
    let CalculateScore (rules, player1, player2) =
        let rolls = player1.Rolls + player2.Rolls
        let loser = if player1.Score >= rules.TargetScore then player2 else player1
        loser.Score * rolls
    
type Day21() =
    interface ISolver with
        member this.SolvePartOne(s: string) =
            Day21.Parse s |> Day21.PlayPartOne |> Day21.CalculateScore |> string
            
        member this.SolvePartTwo(s: string) = ""
