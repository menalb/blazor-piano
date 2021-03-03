import * as Tone from 'tone'

const synth = new Tone.Synth().toDestination();

export function play(note) {
    synth.triggerAttackRelease(note, "8n");
}


export function keyAttack(note) {
    synth.triggerAttack(note);
}


export function keyRelease() {
    synth.triggerRelease(Tone.now());
}