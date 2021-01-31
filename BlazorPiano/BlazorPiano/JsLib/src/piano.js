import * as Tone from 'tone'

export function play(note) {
    const synth = new Tone.Synth().toDestination();

    synth.triggerAttackRelease(note, "8n");
}