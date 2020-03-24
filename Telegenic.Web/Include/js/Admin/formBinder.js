import { formBase } from "./formBase";

export class formBinder extends formBase {
    constructor(route, searchFormName, saveFormName) {        
        super(route, searchFormName, saveFormName);
    }

    bindEditButton() {
        super.addEditEntityHandler();
    }

    bindDeleteButton() {
        super.addDeleteEntityHandler();
    }

    bindSaveButton() {
        super.addSaveEntityHandler();
    }

    bindSearchButton() {
        super.addSearchEntitiesHandler();
    }

    bindResetButton() {
        super.addResetPageHandler();
    }

    bindAddButton() {
        super.addAddEntityHander();
    }

    resetAllPanels() {
        formBinder.resetAllPanels();
    }

    bindInit() {
        this.bindEditButton();
        this.bindDeleteButton();
        this.bindSaveButton();
        this.bindSearchButton();
        this.bindResetButton();
        this.bindAddButton();
    }
}