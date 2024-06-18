INCLUDE Globals.ink
VAR numQuestions = 0
This was one of our longest trips. We were in a very foreign country, but we had an amazing time. We never imagined we’d go somewhere so exotic!#speaker:Harold #type:external
->questions_return

===questions_return===
{
    -numQuestions<2:
       It sounds like this was a very significant trip. It’s killing me that I can’t even recognize this photo.#speaker:Diane #type:internal
  - numQuestions<3:
         I can’t believe I used to be a person who was so bold and worldly. I’ll never be that person again. #speaker:Diane #type:internal
   - else:
         Do you remember where we were when we took that picture, honey?#speaker:Harold #type:external
}

*[How far away was this place?]
This was one of the longest trips we ever took. It took us almost a full day to get there.#speaker:Harold #type:external
    ~numQuestions=numQuestions+1
    ~nextVersion(true)
    ->questions_return
*[When did we go here?]
    We went here in 2009, the year after they hosted the Olympics.#speaker:Harold #type:external
    ~numQuestions=numQuestions+1
    ~nextVersion(true)
    ->questions_return
*[What language do they speak here?]
    There are a few different languages spoken here, and you actually learned a little bit of one, but it was tricky. I remember at one restaurant you tried to order some dumplings, but you actually asked the waiter if you could go to sleep! #speaker:Harold #type:external
    ~numQuestions=numQuestions+1
    ~nextVersion(true)
    ->questions_return
*->
   
    ~enterGuessMode(true)
    ~snowGlobeCompleted = true
    ->holdingKnot
    

===holdingKnot===
    I feel like I should remember such a long trip #speaker:Diane #type:internal
    ->holdingKnot
    
===correctAnswer===
You remember! Yes, we went to Beijing after being captivated by China during the 2008 Summer Olympics. And we weren’t the only ones, it seemed like people all over the world became fascinated with China at that time.#speaker:Harold #type:external
Right, Beijing. I remember that now, it was an exhausting trip but so fascinating. #speaker:Diane #type:internal
~numCorrectGuesses=numCorrectGuesses+1
~objectsCompleted=objectsCompleted+1
->END
===incorrectAnswer===
    Oh, no, sorry Honey, we got this in Beijing in China. They hosted the Summer Olympics in 2008 and we were obsessed with going after seeing so much of the country.#speaker:Harold #type:external
    That's right, we went after the Winter Olympics. It's very cold there.#speaker:Diane
    They did host the Winter Olympics a few years ago, but we went after the Summer Olympics in 2008. #speaker:Harold
    Oh my gosh, we were just talking about the Summer Olympics. I’m so embarrassed.#speaker:Diane #type:internal
~objectsCompleted=objectsCompleted+1
->END
