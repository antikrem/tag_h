import { FilesController } from "../Controllers/FilesController";
import { FileTagsController } from "../Controllers/FileTagsController";
import { LoggingController } from "../Controllers/LoggingController";
import { TagsController } from "../Controllers/TagsController";

export { Controllers };

class ControllerType {
    Files!: FilesController;
    FileTags!: FileTagsController;
    Logging!: LoggingController;
    Tags!: TagsController;
};

let Controllers : ControllerType = {
    Files: null!,
    FileTags: null!,
    Logging: null!,
    Tags: null!
};