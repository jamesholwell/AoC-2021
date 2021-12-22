namespace AoC2021.FSharp

open AoC2021.Core

module Day21 =
    type GameRules = {
        BoardSize: int
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
    
    let PlayGame rules die player1 player2 =
        let roller = die |> DieRoller rules.NumberOfRolls
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
            NumberOfRolls = 3
            TargetScore = 1000
        }
        let die = DeterministicDie 100
        let player1 = { Name = "Player 1"; Location = pl1 ; Score = 0 ; Rolls = 0 }
        let player2 = { Name = "Player 2"; Location = pl2 ; Score = 0 ; Rolls = 0 }
        PlayGame rules die player1 player2
    
    let CalculateScore (rules, player1, player2) =
        let rolls = player1.Rolls + player2.Rolls
        let loser = if player1.Score >= rules.TargetScore then player2 else player1
        loser.Score * rolls
    
    let (++) (a, b) (c, d) = (a + c, b + d)
    
    let MultiplyAndSwap (x: uint64) (a, b) = (x * b, x * a)
    
    let PlayQuantumGame rules player1 player2 =
        let rec loop thisPlayer thisPlayerWins thatPlayer thatPlayerWins =
            let roll3 = TakeTurn rules thisPlayer 3 // 1 universes
            let roll4 = TakeTurn rules thisPlayer 4 // 3 universes
            let roll5 = TakeTurn rules thisPlayer 5 // 6 universes
            let roll6 = TakeTurn rules thisPlayer 6 // 7 universes
            let roll7 = TakeTurn rules thisPlayer 7 // 6 universes
            let roll8 = TakeTurn rules thisPlayer 8 // 3 universes
            let roll9 = TakeTurn rules thisPlayer 9 // 1 universes
            
            let roll3Outcome = if roll3.Score >= rules.TargetScore then (1UL, 0UL) else MultiplyAndSwap 1UL (loop thatPlayer thatPlayerWins roll3 thisPlayerWins)
            let roll4Outcome = if roll4.Score >= rules.TargetScore then (3UL, 0UL) else MultiplyAndSwap 3UL (loop thatPlayer thatPlayerWins roll4 thisPlayerWins)
            let roll5Outcome = if roll5.Score >= rules.TargetScore then (6UL, 0UL) else MultiplyAndSwap 6UL (loop thatPlayer thatPlayerWins roll5 thisPlayerWins)
            let roll6Outcome = if roll6.Score >= rules.TargetScore then (7UL, 0UL) else MultiplyAndSwap 7UL (loop thatPlayer thatPlayerWins roll6 thisPlayerWins)
            let roll7Outcome = if roll7.Score >= rules.TargetScore then (6UL, 0UL) else MultiplyAndSwap 6UL (loop thatPlayer thatPlayerWins roll7 thisPlayerWins)
            let roll8Outcome = if roll8.Score >= rules.TargetScore then (3UL, 0UL) else MultiplyAndSwap 3UL (loop thatPlayer thatPlayerWins roll8 thisPlayerWins)
            let roll9Outcome = if roll9.Score >= rules.TargetScore then (1UL, 0UL) else MultiplyAndSwap 1UL (loop thatPlayer thatPlayerWins roll9 thisPlayerWins)
            
            roll3Outcome
            ++ roll4Outcome
            ++ roll5Outcome
            ++ roll6Outcome
            ++ roll7Outcome
            ++ roll8Outcome
            ++ roll9Outcome
        loop player1 0 player2 0
    
    let PlayPartTwo (pl1, pl2) =
        let rules = {
            BoardSize = 10
            NumberOfRolls = 3
            TargetScore = 21
        }
        let player1 = { Name = "Player 1"; Location = pl1 ; Score = 0 ; Rolls = 0 }
        let player2 = { Name = "Player 2"; Location = pl2 ; Score = 0 ; Rolls = 0 }
        let numberOfPlayer1Wins, numberOfPlayer2Wins = PlayQuantumGame rules player1 player2
        if (numberOfPlayer1Wins > numberOfPlayer2Wins) then numberOfPlayer1Wins else numberOfPlayer2Wins
    
type Day21() =
    interface ISolver with
        member this.SolvePartOne(s: string) =
            Day21.Parse s |> Day21.PlayPartOne |> Day21.CalculateScore |> string
            
        member this.SolvePartTwo(s: string) =
            Day21.Parse s |> Day21.PlayPartTwo |> string
