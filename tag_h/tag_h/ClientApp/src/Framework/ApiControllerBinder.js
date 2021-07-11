import { Controllers } from './Controllers'

export default async function bindApiControllers() {
    var controllerSchemas = await loadControllerSchema();
    controllerSchemas.forEach(bindController);
}

async function loadControllerSchema() {
    const response = await fetch('/ApiControllerBinder/Get');
    return await response.json();
}

function bindController(controllerSchema) {
    var name = controllerSchema.name;
    Controllers[name] = function () { }
    Controllers[name].prototype = {}

    controllerSchema.methods.forEach(
        methodSchema => {
            Controllers[name][methodSchema.name] = generateMethod(controllerSchema, methodSchema);
        }
    )
}

function generateMethod(controllerSchema, methodSchema) {

    return async function (...parameters) {
        if (methodSchema.parameters.length !== methodSchema.parameters.length) {
            throw new Error('Invalid number of arguments');
        }
        
        const response = await fetch(
            `/${controllerSchema.name}/${methodSchema.name}${createParameterURL(methodSchema, parameters)}`,
            createResponseBody(methodSchema, parameters)
        );
        return await response.json();
    }
}

function createParameterURL(methodSchema, parameters) {
    var url = [];
    for (var i = 0; i < parameters.length; i++) {
        if (methodSchema.parameters[i].isSimple) {
            url.push(`${methodSchema.parameters[i].name}=${parameters[i]}`)
        }
    }
    return url.length ? "?" + url.join("&") : "";
}

function createResponseBody(methodSchema, parameters) {
    var i = methodSchema.parameters.findIndex(parameter => !parameter.isSimple);
    return {
        method: methodSchema.method,
        ...(i >= 0 &&
        {
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(parameters[i])
        }),
    };
}