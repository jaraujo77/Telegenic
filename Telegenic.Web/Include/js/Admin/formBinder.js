import { formBase } from "./formBase";

export class formBinder extends formBase {    

    bindEditButton() {
        super.setBtnEditClickHandler();
    }

    bindDeleteButton() {
        super.setBtnDeleteClickHandler();
    }

    bindSaveButton() {
        super.setBtnSaveClickHandler();
    }

    bindSearchButton() {
        super.setBtnSearchClickHandler();
    }

    bindResetButton() {
        super.setBtnResetClickHandler();
    }

    bindAddButton() {
        super.setBtnAddClickHandler();
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