import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ClientsService {

  constructor(private Httpclient:HttpClient) { }


  getAllClients():Observable<any>{
    return this.Httpclient.get("https://localhost:7004/api/GetAll");
  }
  getClientByid(id:number|null):Observable<any>{
    return this.Httpclient.get(`https://localhost:7004/api/Client/${id}`);
  }
  addClient(myobj:object):Observable<any>{
    return this.Httpclient.post(`https://localhost:7004/api/Client`,myobj);
  }
  updateClient(id:number|null,myobj:object):Observable<any>{
    return this.Httpclient.put(`https://localhost:7004/api/Client/${id}`,myobj);
  }
  deleteClient(id:number):Observable<any>{
    return this.Httpclient.delete(`https://localhost:7004/api/Client/${id}`);
  }
  searchByfname(fname:string):Observable<any>{
    return this.Httpclient.get(`https://localhost:7004/api/FilterByFname/${fname}`);
  }
  searchBylname(lname:string):Observable<any>{
    return this.Httpclient.get(`https://localhost:7004/api/FilterByLname/${lname}`);
  }
  getByPaging(pageIndex:number,pageSize:number):Observable<any>{
  return this.Httpclient.get(`https://localhost:7004/api/GetByPaging?pageIndex=${pageIndex}&pageSize=${pageSize}`);
  }
}
