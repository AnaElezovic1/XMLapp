export interface Accommodation {
  id: Number;
  name: string;
  description: string;
  location: string;
  images: string;
  beds: number;
  hostId: number;
  [key: string]: any;
}