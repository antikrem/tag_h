import { asyncEvent, event, reduce, reduced } from "event-reduce";
import { reactive } from "event-reduce-react";
import React, { useEffect } from 'react';
import { ImagesTiling } from "../components/Images/ImagesTiling";
import { ImageTileModel } from "../components/Images/ImageTile";
import { TaskPaneContainer } from "../components/TaskPane/TaskPane";
import { Controllers } from "../Framework/Controllers";
import { TagsModel } from "../Tags/TagsModel";
import { ImageViewModel } from "../Typings/ImageViewModel";
import { Tag } from "../Typings/Tag";
import { PageModel } from "./../PageSystem/PageModel";
import { ImageDetailsPane } from "./ImageDetailsPane";

export class ImagesEvents {
    fetchImages = asyncEvent<ImageViewModel[]>();
    selectImage = event<ImageModel>();
}

export class ImagesModel extends PageModel {
    name = "Images";

    events = new ImagesEvents();

    @reduced
    images = reduce([] as ImageModel[], this.events)
        .on(e => e.fetchImages.resolved, (_, { result: images }) => images.map(image => new ImageModel(image)))
        .value;

    @reduced
    selectedImage = reduce(null as ImageModel | null, this.events)
        .on(e => e.selectImage, (selection, image) => selection == image ? null : image)
        .value;

    view() {
        return <Images model={this} tags={this.tagsModel} />;
    }

    constructor(public readonly tagsModel: TagsModel) {
        super();
    }
}

class ImageEvents {
    addTag = event<Tag>();
    removeTag = event<Tag>();
}

export class ImageModel implements ImageTileModel {

    events = new ImageEvents();

    get id() {
        return this.image.id;
    }

    get src() {
        return `/Images/GetFile?imageId=${this.id}`;
    }

    @reduced
    tags = reduce(this.image.tags, this.events)
        .on(e => e.addTag, (tags, tag) => [...tags, tag])
        .on(e => e.removeTag, (tags, tag) => tags.filter(t => t.id != tag.id))
        .value;

    constructor(private readonly image: ImageViewModel) {
    }
}

export const Images = reactive(function Images({ model, tags }: { model: ImagesModel, tags: TagsModel }) {
    useEffect(() => model.events.fetchImages(Controllers.Images.GetAll()), [model])

    let panes = Array.from(getPanes(model, tags));

    return (
        <TaskPaneContainer active={model.selectedImage != null} panes={panes}>
            <ImagesTiling
                tiledImages={model.images}
                setImage={image => model.events.selectImage(image)} />
        </TaskPaneContainer>
    );
});

function* getPanes(model: ImagesModel, tags: TagsModel) {
    if (model.selectedImage)
        yield { name: "Image", pane: <ImageDetailsPane model={model.selectedImage} tags={tags} /> };
}