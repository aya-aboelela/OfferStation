export class ApiResponce{
    constructor(
        public statusCode:number,
        public message:string,
        public success:boolean,
        public data:any,
    ){}
}