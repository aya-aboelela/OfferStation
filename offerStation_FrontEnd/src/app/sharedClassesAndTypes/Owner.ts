export class Owner{
    constructor(
        FirstName:string,
        LastName:string,
        Phone:string,
        Address:string[],
        Password:string,
        Email:string,
        OwnerCategoryId:number
    ){}
}

export class Seller{
    constructor(
        public id:number,
        public  name:string,
        public rating:number,
        public image:any,
       
    ){}
}