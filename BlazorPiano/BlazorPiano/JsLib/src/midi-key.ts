import WebMidi from 'webmidi'

export const init = (log: (message: string) => void) => {
        
    WebMidi.enable(() => {        
        console.log(WebMidi.outputs);
        WebMidi.outputs.forEach(device => {
            log(device.name);            
        });
    })
}