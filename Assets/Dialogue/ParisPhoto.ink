INCLUDE Globals.ink
VAR numVists = 0
VAR numQuestions = 0
VAR completed = false

{parisPhotoVisits >0: ->questions_start|->game_start}

=== game_start ===
Come on Honey, let’s do this exercise. #speaker:Harold
It’s such a lovely day out though, Bear. Can’t we just go for a walk? #speaker:Diane
Dr. Wasserman said that we should do memory puzzles at least once a day. Now come on, let’s try at least one picture. #speaker:Harold
Ohhhh alright. #speaker:Diana
~parisPhotoVisits=parisPhotoVisits+1
//Fade to black and fade up on the photo
->questions_start

===questions_start===
{parisPhotoCompleted: ->returing_after_finished }
Now where did we take this picture?#speaker:Harold
->questions_return

===questions_return===
{
    -numQuestions<2:
        Sheesh, I don’t know what this is a picture of. How is this happening? Why can’t I recognize it? I’m so stupid! #speaker:Diane #type:internal
  - numQuestions<3:
        I think I’m starting to recognize it, but I’m going to ask one more question. I don’t want to embarrass myself by getting it wrong. #speaker:Diane #type:internal
   - else:
        That’s enough hints. Do you remember where we took this picture?#speaker:Harold #type:external
}


*[What’s this a picture of?]
    If I tell you that you’ll know it for sure! Let’s just say it’s one of the most famous buildings in Europe.#speaker:Harold #type:external
    ~numQuestions=numQuestions+1
    ->questions_return
*[When did we go here?]
    We went here for our 30th wedding anniversary. Our friends had told us it’s one of the most romantic cities in the world, and they were right!#speaker:Harold #type:external
    ~numQuestions=numQuestions+1
    ->questions_return
*[What did we do here?]
    We went to a cute bistro for dinner that night. We may have had one too many bottles of wine, and I’m pretty sure this was the night we tried frog legs. I hated them, but you liked them!#speaker:Harold #type:external
    ~numQuestions=numQuestions+1
    ->questions_return
* ->
//this line calls a function in Unity to enter the mode for guessing the place
    ~enterGuessMode(true)
    ~parisPhotoCompleted = true
    -> holdingKnot
    
===holdingKnot===
Now where did we take this picture?#speaker:Harold
Where was it?#speaker:Diane #type:internal
->holdingKnot
    
    
===correctAnswer===
That’s right, Paris! This was one of my favorite trips we took, even if I was too chicken to go to the top of the Eiffel Tower.#speaker:Harold #type:external
 I knew it! Okay, maybe my memory is still working pretty well.#speaker:Diane
 ~numCorrectGuesses=numCorrectGuesses+1
 ~objectsCompleted=objectsCompleted+1
 ->END
 
===incorrectAnswer===
Nope, not there. This picture was taken in Paris, France. We took it almost 15 years ago. #speaker:Harold
Oh right, I knew that. #Diane
Why didn’t I know that? I know what the Eiffel Tower is. I was a geography teacher for crying out loud! #type:internal
~objectsCompleted=objectsCompleted+1
    -> END
===returing_after_finished===
    Paris sure was great (test dialogue) #speaker:Harold #type:external
->END

