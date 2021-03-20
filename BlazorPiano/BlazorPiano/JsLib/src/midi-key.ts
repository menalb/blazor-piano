import WebMidi from 'webmidi'

export const init = (
    log: (deviceName: string) => void,
    play: (note: MidiNote) => void,
) => {

    WebMidi.enable(() => {
        let deviceName: string = '';
        WebMidi.inputs.forEach(device => {
            log(device.name);
            deviceName = device.name;
        });
        if (deviceName) {
            const input = WebMidi.getInputByName(deviceName);
            if (input !== false) {
                input.addListener('noteon', "all", function (e) {
                    play({ name: e.note.name, octave: e.note.octave });
                });
            }
        }
    })
}

export interface MidiNote {
    name: string;
    octave: number;
}