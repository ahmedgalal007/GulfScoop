export class Site {

    public static init(_param1: number): string {
        return "";
    }

    public header: string = "";

    public getHeader(_selector: string): string {
        return "";
    }

    public getFooter(_selector: string): string {
        $.getJSON("url", () => { });
        return "";
    }
}

console.log(Site.init( 1000));
