import { asyncEvent, derived, event, reduce, reduced } from "event-reduce";
import { reactive } from "event-reduce-react";
import * as React from 'react';
import { ImagesTiling } from "../components/Images/ImagesTiling";
import { ImageTileModel } from "../components/Images/ImageTile";
import { TaskPaneContainer } from "../components/TaskPane/TaskPane";
import { PageModel } from "./../PageSystem/PageModel";
import { ImportPane } from "./ImportPane";



export class ImportEvents {
    addFiles = event<File[]>();
    submitImages = asyncEvent();
}

export class ImportModel extends PageModel {
    name = "Import";

    events = new ImportEvents();

    @reduced
    selectedFiles = reduce([] as File[], this.events)
        .on(e => e.addFiles, (files, newFiles) => [...files, ...newFiles])
        .on(e => e.submitImages.resolved, _ => [])
        .value;

    @derived
    get selectedImages() {
        return this.selectedFiles.map(file => new ImportTileImageModel(file));
    }

    view() {
        return <Import model={this} />;
    }
}

class ImportTileImageModel implements ImageTileModel {
    src = URL.createObjectURL(this.file);

    constructor(private readonly file: File) {
    }
}

export const Import = reactive(function Import({ model }: { model: ImportModel }) {
    return (
        <TaskPaneContainer active={true} panes={Array.from(getPanes(model))}>
            <ImagesTiling tiledImages={model.selectedImages} />
        </TaskPaneContainer>
    );
});

function* getPanes(model: ImportModel) {
    yield { name: "Import", pane: <ImportPane model={model} /> };
}