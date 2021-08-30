import ViewModel from "react-use-controller";

export default class SideBarViewModel extends ViewModel {
    
    collapsed = false;

    toggleCollapsed = () => {
        this.collapsed = !this.collapsed;
    }
}