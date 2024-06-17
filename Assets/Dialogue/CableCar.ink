INCLUDE Globals.ink
VAR numVists = 0
VAR numQuestions = 0
VAR completed = false

===questions_start===

Ahh, that was a fun trip that we took when we were young, before we were even married. Do you remember where we got this? #speaker:Harold

I can't even tell what this thing is. What the heck am I looking at? #speaker:Diane #type:internal
->questions_return

===questions_return===
{
    -numQuestions<1: C’mon Diane, you know this. It’s a place in the US… but… gosh, it’s at the tip of my tongue but it won’t come out. #speaker:Diane #type:internal
}

*[What is this thing?]
    This is a vehicle. I don't know if people actually use them anymore, but when we were there, this was the best way to get up and down the steep hills.#speaker:Harold #type:external
        ~numQuestions=numQuestions+1
        ->questions_return
*[What else did we do here?]
    We were here in the 70s which was a really special time for this city. We went to poetry readings, music cafes, art galleries... the city was just exploding with culture at that time. #speaker:Harold
        ~numQuestions=numQuestions+1
        ->questions_return
*[Was this place far away?]
    It was very far from where we lived then: New Jersey. But now that we live in Arizona, it's much closer, but still a few hours away by plane.#speaker:Harold
    ~numQuestions=numQuestions+1
    ->questions_return
*->
    ~enterGuessMode(true)
    ~cableCarCompleted = true
    ->holdingKnot
    
===holdingKnot===
I think that should give you a pretty good idea of where we got this. Do you remember where? #speaker:Harold
->holdingKnot

===correctAnswer===
That's right, San Francisco! Good job, Honey, I knew you could do it! #speaker:Harold
He's treating me like a child! I knew it was San Francisco! Have I really gotten so forgetful that he thinks I don't remember our trip to California? #speaker:Diane #type:internal
->END

===incorrectAnswer===
Oh... no Honey, we got this in San Francisco. We went here when we were in our 20s, so it was a long time ago. Maybe this one is too hard. #speaker:Harold
I thought the doctor said that it's mostly my short-term memoery that will be affected at this point... am I losing my long-term memory already? #speaker:Diane #type:internal
->END