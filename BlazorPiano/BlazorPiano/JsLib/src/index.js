import { init, keyAttack, keyRelease, play } from './piano';

export function initPiano() {
    init('piano');
}

export function initSynth() {
    init('');
}

export function playNote(note) {
    play(note);
}

export function attack(note) {
    keyAttack(note);
}

export function release(note) {
    keyRelease();
}