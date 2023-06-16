export interface Booking {
    id: Number;
    start: Date;
    end: Date;
    price: number;
    perperson: boolean;
    autoaccept: boolean;
    accommodationId: number;
  }