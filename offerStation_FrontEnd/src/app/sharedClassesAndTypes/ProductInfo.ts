export class ProductInfo {
    constructor(
        public id: number,
        public name: string,
        public description: string,
        public price: any,
        public discount: number,
        public discountPrice: any,
        public image: any,
        public categoryId: number,
        // public categoryName:string,
    ) { }
}
export class ProductDetails {
    constructor(
        public name: string,
        public description: string,
        public price: any,
        public discount: number,
        public image: any,
        public categoryId: number,
    ) { }
}