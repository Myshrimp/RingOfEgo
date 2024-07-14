

public class ParClip_c : data_C<ParClip> { 

    // Start is called before the first frame update
   public  override   void Start()
    {
        base.Start(); 
        foreach(var i in datas)
        {
            i.do_StartByClip(this);
        }
    }

    // Update is called once per frame
   
}
