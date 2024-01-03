export class Product {
    constructor(
        public id: number,
        public name: string,
        public description: string,
        public price: any,
        public prefPrice: any,
        public ownerId: number,
        public image:any,
        public traderImage:any

    ) { }
}
export class OfferProducts {
    constructor(
        public name: string,
        public description: string,
        public price: number,
        public image:any,
        public quantity:number

    ) { }
}