import { Letter, LetterStatus } from '@/scripts/letter'
import { Word } from '@/scripts/word';
import { ref, reactive } from 'vue'
import { WordsService } from './wordsService';

export class WordleGame {

    constructor(secretWord?: string | null) {
        this.restartGame(secretWord)
    }

    guesses = new Array<Word>()
    secretWord = ''

    //check guess length
    // if (this.text.length !== secretWord.length) {
    //     console.log('wrong length')
    //     return false
    // }

    restartGame(secretWord?: string | null) {
        this.secretWord = secretWord || WordsService.getRandomWord()
        this.guesses.splice(0)
    }

    submitGuess(guess: string) {    
        if( WordsService.isValidWord(guess.toLowerCase()) === false) {
            alert('invalid word: ' + guess)
            return
        }
        const word = new Word(guess)
        this.guesses.push(word)
        word.check(this.secretWord)
    }
}