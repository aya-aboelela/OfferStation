
export class CustomerOrders {

    constructor(
      public id: number,
      public orderStatus: orderStatus,
      public orderDate: Date,
      public traderId: number,
      public total: any,
      public products: OrdersProduct[],
      public offers: OrdersOffer[],
      public traderName:string

    ) { }
}

export enum orderStatus {
    pending,
    ordered,
    shipped, 
    delivered
}
export class OrdersProduct {

    constructor(
        public id:number,
        public orderId :number,
        public traderProductId :number,
        public quantity :number,
    ){}
}

export class OrdersOffer {

    constructor(
        public id:number,
        public orderId :number,
        public traderOfferId :number,
        public quantity :number
    ){}
}


export class RequestedOrders {

    constructor(
      public id: number,
      public orderDate: Date,
      public requesterId: number,
      public orderStatus: orderStatus,
      public netTotal: any,
      public products: OrdersProduct[],
      public offers: OrdersOffer[],
      public requesterName:string

    ) { }
}
export class PendingOrders {

    constructor(
      public id: number,
      public orderDate: Date,
      public traderName: string,
      public adminTotal: any,
      public requesterName:string

    ) { }
}







