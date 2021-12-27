import { asyncEvent, event, reduce, reduced } from "event-reduce";
import { reactive } from "event-reduce-react";
import React, { useEffect } from 'react';
import { ImagesTiling } from "../components/Images/ImagesTiling";
import { ImageTileModel } from "../components/Images/ImageTile";
import { TaskPaneContainer } from "../components/TaskPane/TaskPane";
import { Controllers } from "../Framework/Controllers";
import { ImageViewModel } from "../Typings/ImageViewModel";
import { PageModel } from "./../PageSystem/PageModel";

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
        return <Images model={this} />;
    }
}

export class ImageModel implements ImageTileModel {

    get id() {
        return this.image.id;
    }

    get src() {
        return `/Images/GetFile?imageId=${this.id}`;
    }

    constructor(private readonly image: ImageViewModel) {
    }
}

export const Images = reactive(function Images({ model }: { model: ImagesModel }) {
    useEffect(() => model.events.fetchImages(Controllers.Images.GetAll()), [model])

    return (
        <TaskPaneContainer active={model.selectedImage != null} panes={[]}>
            <ImagesTiling
                tiledImages={model.images}
                setImage={(image) => model.events.selectImage(image)} />
        </TaskPaneContainer>
    );
});