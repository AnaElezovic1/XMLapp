export interface Accommodation {
  id: number;
  name: string;
  description: string;
  location: string;
  images: string;
  beds: number;
  hostId: number;
  [key: string]: any;
}