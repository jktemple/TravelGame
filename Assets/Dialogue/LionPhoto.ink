INCLUDE Globals.ink
VAR numVists = 0
VAR numQuestions = 0
VAR completed = false
->questions_start
{lionPhotoCompleted: ->returing_after_finished}
->questions_start

===returing_after_finished===
    Alright Honey, why don't you pick another souvenier and we'll do the same thing. #speaker:Harold #type:external
    ->END
===questions_start===
Oh this was an incredible trip, but it’s also the most scared I’ve ever been in my life! Do you remember where we took this picture? #speaker:Harold

This is so frustrating, this picture looks like nothing to me. #speaker:Diane #type:internal
->questions_return

===questions_return===
{
    -numQuestions<1: Oh I remember this place having very good wine… but why can’t I remember where it is? #speaker:Diane #type:internal
    -else: It's starting to feel a little more familar...#speaker:Diane #type:internal
}

*[What is this?]
    That’s an animal, Honey. We saw it from the jeep we were in.#speaker:Harold #type:external
        ~numQuestions=numQuestions+1
        ~nextVersion(true)
        ->questions_return
*[Why were you so scared?]
   Well, that animal had just walked past the jeep. The guide said that they usually ignore humans, so I wasn’t expecting it when it roared and took off running right as it was passing by! I nearly jumped out of my skin! #speaker:Harold
        ~numQuestions=numQuestions+1
        ~nextVersion(true)
        ->questions_return
*[Did we take the kids?]
   The kids were the reason why we took the trip in the first place! Adam was 10 and was obsessed with Simba. #speaker:Harold
    ~numQuestions=numQuestions+1
    ~nextVersion(true)
    ->questions_return
*->
    ~enterGuessMode(true)
    ~lionPhotoCompleted = true
    ->holdingKnot
    
===holdingKnot===
Okay Honey, do you know where this photo was taken? #speaker:Harold #type:external

->holdingKnot

===correctAnswer===
That’s right, South Africa! After Adam saw the Lion King he insisted that we go on a safari to see real-life lions. Kristen was a little older and pouted for the first half of the trip, but as soon as she saw the lions and elephants, she was immediately obsessed #speaker:Harold #type:external
She says it was this trip that inspired her to get into nature photography: she’s the one who took this picture! #speaker:Harold
I knew it was South Africa! And I remember drinking really good wine there too. #speaker:Diane
Yes! For years after this trip, you would only buy South African wines. #speaker:Harold
~numCorrectGuesses=numCorrectGuesses+1
~objectsCompleted = objectsCompleted+1
->END

===incorrectAnswer===
Oh, sorry Honey, that picture was taken in South Africa. After Adam saw the Lion King he insisted that we go on a safari to see real-life lions. Kristen was a little older and pouted for the first half of the trip, but as soon as she saw the lions and elephants, she was immediately obsessed. #speaker:Harold
She says it was this trip that inspired her to get into nature photography: she’s the one who took this picture! #speaker:Harold
 I remember Adam is my son… but who is Kristen? My daughter’s name is Diane. #speaker:Diane
 No, that’s your name, Honey. #speaker:Harold
 Oh right, my daughter is... Kristen. #speaker:Diane
 Exactly right. #speaker:Harold
 ~objectsCompleted = objectsCompleted+1
->END