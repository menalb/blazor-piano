import { keyAttack, keyRelease, play } from './piano';

export function playNote(note) {
    play(note);
}

export function attack(note) {
    keyAttack(note);
    console.log("attack " + note);
}

export function release(note) {
    keyRelease();
    console.log("release " + note);
}