var logger = DebugLogger;
var print = logger.log;

Events.Listen('init', () => { print("Initialised Scripting Engine") })
help(logger)
list()