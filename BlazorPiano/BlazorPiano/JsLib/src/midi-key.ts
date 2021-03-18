import webmidi from 'webmidi';
import WebMidi, { Input } from 'webmidi'

export const init = (
    log: (message: string) => void,
    play: (note: string) => void,
) => {

    WebMidi.enable(() => {
        let deviceName: string = '';
        WebMidi.inputs.forEach(device => {
            log(device.name);
            deviceName = device.name;
        });
        if (deviceName) {
            const input = webmidi.getInputByName(deviceName);
            if (input !== false) {
                input.addListener('noteon', "all", function (e) {
                    play(e.note.name + e.note.octave);
                });
            }
        }
    })
}