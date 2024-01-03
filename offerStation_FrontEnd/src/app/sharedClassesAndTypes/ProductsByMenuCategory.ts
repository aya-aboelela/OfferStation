export class ProductsByMenuCategory {
    [x: string]: any;
    constructor(
        public id: number,
        public name: string,
        public price: any,
        public description: string,
        public discountPrice: any,
        public discount: number,
        public image: any
    ) { }
}