

export class Offer {
    constructor(

        public id: number,
        public ownerId: number,
        public name: string,
        public description: string,
        public price: number,
        public image: string,
        public createdTime: string,
        public prefPrice: number,
        public traderImage: string,
        public products: OfferProduct[],

    ) { }
}

export class OfferProduct {
    constructor(
        public quantity: number,
        public productId: number,
    ) { }

}


