import { init, keyAttack, keyRelease, play } from './piano';
import { init as initMidiKey } from './midi-key';


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
    keyRelease(note);
}

export function initMidi(dotNetObject: DotNet.DotNetObject) {
    initMidiKey(
        deviceName => dotNetObject.invokeMethodAsync('LogDevice', deviceName),
        note=> dotNetObject.invokeMethodAsync('PlayNote', note),
        );
}