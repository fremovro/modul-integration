namespace MyISolver

type Class1() = 
    interface ISolver with
        member this.GetName() = "Жадный алгоритм"
        member this.Solve(M, mWeight, mCost) =
            let weight_lst = List.ofArray mWeight
            let cost_lst = List.ofArray mCost
            let rec fill_lst my_lst weight cost icount =
                if weight=[] || cost=[] then my_lst
                else
                    let n = (float)cost.Head / (float)weight.Head
                    fill_lst ((n, weight.Head, icount)::my_lst) weight.Tail cost.Tail (icount+1)
            let rec get_max my_lst max =
                if my_lst = [] then max
                else
                    let n, w, i = my_lst.Head
                    if n > max then get_max my_lst.Tail n
                    else get_max my_lst.Tail max
            let rec get_lst my_lst my_tail res_lst m  M =
                if my_lst = [] then 
                    if res_lst = [] then []
                    else res_lst
                else
                    let n, w, i = my_lst.Head
                    if (float)n = (get_max my_lst 0.00) then
                        if M >= (m + w) then get_lst (my_tail @ my_lst.Tail) [] (i::res_lst) (m + w) M
                        else get_lst (my_tail @ my_lst.Tail) [] res_lst m M
                    else get_lst my_lst.Tail (my_lst.Head::my_tail) res_lst m M
            List.toArray (get_lst (fill_lst [] weight_lst cost_lst 0) [] [] 0 M)
