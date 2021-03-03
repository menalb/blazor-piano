import * as Tone from 'tone'

const synth = new Tone.Sampler({
    urls: {
        "C4": "C4.mp3",
        "D#4": "Ds4.mp3",
        "F#4": "Fs4.mp3",
        "A4": "A4.mp3",
    },
    release: 1,
    baseUrl: "https://tonejs.github.io/audio/salamander/",
}).toDestination();

export function play(note) {
    synth.triggerAttackRelease(note, "8n");
}


export function keyAttack(note) {
    synth.triggerAttack(note);
}


export function keyRelease() {
    synth.triggerRelease(Tone.now());
}