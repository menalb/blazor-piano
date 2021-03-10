import { init, keyAttack, keyRelease, play } from './piano';

export function initPiano() {
    init('piano');
}

export function initSynth() {
    init('');
}

export function playNote(note: string) {
    play(note);
}

export function attack(note: string) {
    keyAttack(note);
}

export function release(note: string) {
    keyRelease();
}