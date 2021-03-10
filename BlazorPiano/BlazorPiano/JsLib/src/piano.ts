import * as Tone from 'tone'

let synth = createSynth().toDestination();

export function init(type: string) {
    if (type === 'piano') {
        synth = createPiano().toDestination()
    } else {
        synth = createSynth().toDestination();
    }
}

function createSynth() {
    return new Tone.Synth();
}

function createPiano() {
    return new Tone.Sampler({
        urls: {
            "C4": "C4.mp3",
            "D#4": "Ds4.mp3",
            "F#4": "Fs4.mp3",
            "A4": "A4.mp3",
        },
        release: 1,
        baseUrl: "https://tonejs.github.io/audio/salamander/",
    })
}

export function play(note: string) {
    synth.triggerAttackRelease(note, "8n");
}


export function keyAttack(note: string) {
    synth.triggerAttack(note);
    console.log('attack ' + note);
}


export function keyRelease() {
    synth.triggerRelease(Tone.now());
    console.log('release ');
}