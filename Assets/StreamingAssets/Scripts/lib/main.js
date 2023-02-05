//#pragma GLOBAL_SCOPE
var logger = DebugLogger;
var print = logger.log;

Events.Listen('init', () => { print("Initialised Scripting Engine") })
list() 