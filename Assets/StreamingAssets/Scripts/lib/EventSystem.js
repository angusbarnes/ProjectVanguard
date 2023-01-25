class EventSystem {
    constructor() { 
        this.message = 'Test';
        this.listeners = {};
    }

    Listen(eventName, callback) {
        if (this.listeners[eventName] == null) {
            this.listeners[eventName] = [callback];
            return;
        } 
        
        this.listeners[eventName].push(callback)
    }

    RaiseEvent(eventName) {
        if (this.listeners[eventName] == null) {
            return;
        } 

        for (var callback of this.listeners[eventName]) {
            callback()
        }
    }
}
var Debug = new DebugLogger();
var Events = new EventSystem();
Events.Listen('init', () => { Debug.Log('Event System Initialised')})