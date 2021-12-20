import { ImagesController } from "../Controllers/ImagesController";
import { ImageTagsController } from "../Controllers/ImageTagsController";
import { LoggingController } from "../Controllers/LoggingController";
import { TagsController } from "../Controllers/TagsController";

export { Controllers };

class ControllerType {
    Images!: ImagesController;
    ImageTags!: ImageTagsController;
    Logging!: LoggingController;
    Tags!: TagsController;
};

let Controllers : ControllerType = {
    Images: null!,
    ImageTags: null!,
    Logging: null!,
    Tags: null!
};