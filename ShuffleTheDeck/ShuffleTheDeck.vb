'Payden Hoskins
'RCET2265
'Spring 2025
'ShuffleTheDeck
'https://github.com/PaydenHoskins/ShuffleTheDeck.git

Option Compare Text
Option Explicit On
Option Strict On

Module ShuffleTheDeck

    Sub Main()
        Dim UserInput As String
        'User Input section
        Do
            Console.Clear()
            DeckCounter()
            Console.WriteLine()
            Console.WriteLine("Press ""d"" to draw ball")
            Console.WriteLine("Press ""s"" to Shuffle")
            Console.WriteLine("Press ""q"" to quit")
            UserInput = Console.ReadLine()
            Select Case UserInput
                Case "d"
                    CardCounter()
                Case "s"
                    CardTracker(0, 0,, True)
                    CardCounter(False)
                Case Else
                    'Pass
            End Select
        Loop Until UserInput = "q"
        Console.Clear()
        Console.WriteLine("Bye, bye.")
    End Sub

    Sub CardCounter(Optional Run As Boolean = True)
        'Pulls a card from the deck and counts it
        Dim DiscardPile(,) As Boolean = CardTracker(0, 0, False)
        Dim CardSuit As Single
        Dim CardNumber As Single
        Static DeckCount As Integer
        Dim Number As String = "1"
        If Run And DeckCount < 52 Then
            Do
                CardSuit = RNG(0, 3)
                CardNumber = RNG(0, 12)
            Loop Until DiscardPile(CInt(CardNumber), CInt(CardSuit)) = False Or DeckCount >= 52
            CardTracker(CInt(CardNumber), CInt(CardSuit), True, False)
            FormatCardSuit(CInt(CardSuit))
            DeckCount += 1
        ElseIf Run = False Then
            CardTracker(0, 0, False, True)
            DeckCount = 0
        ElseIf DeckCount = 52 Then
            CardTracker(0, 0, False, True)
            DeckCount = 0
        End If
    End Sub

    Function RNG(Min As Integer, Max As Integer) As Integer
        'Gives us a random card
        Dim Card As Single
        Randomize()
        Card = Rnd()
        Card *= Max + Min
        Card += Min
        Return CInt((Card))
    End Function

    Function CardTracker(CurrentNumber As Integer, CurrentSuit As Integer, Optional Update As Boolean = False, Optional Clear As Boolean = False) As Boolean(,)
        'Tracks the drawn Cards
        Static DrawnCard(12, 3) As Boolean
        If Update Then
            DrawnCard(CurrentNumber, CurrentSuit) = True
        End If
        If Clear Then
            ReDim DrawnCard(12, 3)
        End If
        Return DrawnCard
    End Function

    Sub DeckCounter()
        'Draws the drawn cards display
        Dim Card As String = " |"
        Dim DisplayCards(,) As Boolean = CardTracker(0, 0)
        Dim ColumbWidth As Integer = 4
        Dim Suit() As String = {" S |", " C |", " H |", " D |"}
        For Each word In Suit
            Console.Write(word.PadLeft(2).PadRight(4))
        Next
        Console.WriteLine()
        Console.WriteLine(StrDup(35, "_"))
        For currentNumber = 0 To 12
            For CurrentSuit = 0 To 3
                If DisplayCards(currentNumber, CurrentSuit) Then
                    Card = $"{FormatCardNumber(currentNumber)} |"
                Else
                    Card = " |"
                End If
                Card = Card.PadLeft(ColumbWidth)
                Console.Write(Card)
            Next
            Console.WriteLine()
        Next
        Console.WriteLine($"You Drew a {FormatCardNumber(0, False)} of {FormatCardSuit(0, False)}!")
    End Sub

    Function FormatCardNumber(CardNumber As Integer, Optional Update As Boolean = True) As String
        Static TheNumber As String
        If Update Then
            Select Case CardNumber
                Case 0
                    TheNumber = "A"
                Case 1 To 9
                    TheNumber = CStr(CardNumber + 1)
                Case 10 To 12
                    If CardNumber = 10 Then
                        TheNumber = "J"
                    ElseIf CardNumber = 11 Then
                        TheNumber = "Q"
                    ElseIf CardNumber = 12 Then
                        TheNumber = "K"
                    End If
            End Select
        End If
        Return TheNumber
    End Function

    Function FormatCardSuit(CardSuit As Integer, Optional update As Boolean = True) As String
        Static SuitDrawn As String
        If update Then
            If CardSuit = 0 Then
                SuitDrawn = "Spades"
            ElseIf CardSuit = 1 Then
                SuitDrawn = "Clubs"
            ElseIf CardSuit = 2 Then
                SuitDrawn = "Hearts"
            ElseIf CardSuit = 3 Then
                SuitDrawn = "Diamonds"
            End If
        End If
        Return SuitDrawn
    End Function
End Module
