INCLUDE Globals.ink
VAR numVists = 0
VAR numQuestions = 0
VAR completed = false
->questions_start
===questions_start===

Oh we visited this place in the 90s. It was kind of surreal being there after everything that had happened, but it was a fascinating trip. #speaker:Harold

How old was I in the 90s? I feel like I was young... but I also think we had the kids by then? Ugh, I'm so confused. #speaker:Diane #type:internal
->questions_return

===questions_return===
{
    -numQuestions<1: This must be some kind of toy… why would we get a toy as a souvenir? #speaker:Diane #type:internal
    -else: Just what could this be? And where is it from? #speaker:Diane #type:internal
}
*[What did we see there?]
    Some of the most ornate and decadent palaces in the world. You can see why they overthrew the emperor!#speaker:Harold #type:external
        ~numQuestions=numQuestions+1
        ~nextVersion(true)
        ->questions_return
*[Why is this thing rattling? Is it broken?]
    No, it’s not broken. There’s more of them inside, they just keep getting smaller and smaller. #speaker:Harold #type:external
        ~numQuestions=numQuestions+1
        ~nextVersion(true)
        ->questions_return
*[After everything that happened? What happened?]
     You used to teach about this in school: for a long time our countries weren’t friendly, and this entire part of the world was closed off to Americans. Our countries still aren’t too friendly, but it’s a lot better now than it was before. #speaker:Harold #type:external
    ~numQuestions=numQuestions+1
    ~nextVersion(true)
    ->questions_return
*->
    ~enterGuessMode(true)
    ~nestingDollsCompleted = true
    ->holdingKnot
    
===holdingKnot===
Okay, I think you should have a pretty good idea of where we got these now. Do you want to guess? #speaker:Harold #type:external
->holdingKnot

===correctAnswer===
Yes, St. Petersburg! Lots of Americans wanted to visit Russia after the Berlin Wall fell, and there was some pretty spectacular sightseeing. We went in April which might have been a bit of a mistake: it was quite chilly most of our trip. #speaker:Harold
Because it's so far North. #speaker:Diane
Yes, I think it's the farthest North we've ever traveled. Good memory, Honey!#speaker:Harold
I remember that itgets colder the farther North you go, but I didn't remember it being cold when we went there. As long as I can make connections like that, maybe I can pass as someone who's not losing their mind.#speaker:Diane #type:internal
~numCorrectGuesses=numCorrectGuesses+1
~objectsCompleted=objectsCompleted+1
->END

===incorrectAnswer===
Actually, we got these dolls in St. Petersburg, Russia. We went there after the fall of the Berlin Wall: you said you were always curious to see what it looks like after years of teaching about it, so we went! #speaker:Harold
What did I teach about it? #speaker:Diane
Well, you taught geography, so you taught a lot about the history of Russia and the USSR and how it incorporated so much of Europe and Asia.#speaker:Harold
You’d think I’d remember all of this stuff if I was so educated on it, but I can’t. Am I going to lose everything I’ve ever known? #speaker:Diane #type:internal
~objectsCompleted=objectsCompleted+1
->END