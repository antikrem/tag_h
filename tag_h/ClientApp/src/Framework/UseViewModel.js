import ViewModel from "react-use-controller";

// Creates a ViewModel if entry extends ViewModel
// Otherwise, returns the parameter
export const useViewModel = function(entry, ...args) {
    if (entry.use && entry.using) {
        return entry.use(...args);
    }
    else {
        return entry;
    }
}